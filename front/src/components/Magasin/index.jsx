import React, { useEffect, useState } from "react";
import {
  fetchStoreByUserId,
  fetchStoreMoney,
  fetchSections,
  createSection,
  fetchLeaderboard,
} from "../../services/api";
import Rayon from "../Rayon/index";
import "../../style/Magasin/style.css";

const Magasin = () => {
  const [argent, setArgent] = useState(null);
  const [storeId, setStoreId] = useState(null);
  const [storeName, setStoreName] = useState(null);
  const [sections, setSections] = useState([]);
  const [userId, setUserId] = useState(null);

  // Leaderboard
  const [leaderboard, setLeaderboard] = useState([]);
  const [showLeaderboard, setShowLeaderboard] = useState(false);

  // Ajout rayon
  const [showPopup, setShowPopup] = useState(false);
  const [newType, setNewType] = useState("");

  // Inject Roboto
  useEffect(() => {
    const link = document.createElement("link");
    link.href = "https://fonts.googleapis.com/css2?family=Roboto&display=swap";
    link.rel = "stylesheet";
    document.head.appendChild(link);
    return () => {
      document.head.removeChild(link);
    };
  }, []);

  // Récupère magasin
  useEffect(() => {
    let localUserId = localStorage.getItem("userId");

    if (!localUserId) {
      localUserId = "3645a00c-680d-4dc0-895e-be7ca8605ecc"; // test
      localStorage.setItem("userId", localUserId);
    }

    setUserId(localUserId);

    fetchStoreByUserId(localUserId)
      .then((stores) => {
        if (Array.isArray(stores) && stores.length > 0) {
          const store = stores[0];
          setStoreId(store.id);
          setStoreName(store.name);
          localStorage.setItem("storeId", store.id);
        } else if (stores && stores.id) {
          setStoreId(stores.id);
          setStoreName(stores.name);
          localStorage.setItem("storeId", stores.id);
        } else {
          console.error("Aucun magasin trouvé pour cet utilisateur");
        }
      })
      .catch((err) => console.error("Erreur récupération magasin :", err));
  }, []);

  // Argent et sections
  useEffect(() => {
    if (!storeId) return;

    const refreshArgent = () => {
      fetchStoreMoney(storeId)
        .then((data) => setArgent(data.money ?? data))
        .catch((err) => console.error("Erreur refresh argent :", err));
    };

    const loadSections = () => {
      fetchSections(storeId)
        .then((data) => {
          const filtered = data.filter((s) => s.storeId === storeId);
          setSections(filtered);
        })
        .catch((err) => console.error("Erreur chargement sections :", err));
    };

    refreshArgent();
    loadSections();

    const interval = setInterval(refreshArgent, 3000);
    return () => clearInterval(interval);
  }, [storeId]);

  // Leaderboard
  useEffect(() => {
    fetchLeaderboard()
      .then((data) => {
        const sorted = data.sort((a, b) => b.score - a.score);
        setLeaderboard(sorted);
      })
      .catch((err) => console.error("Erreur leaderboard :", err));
  }, []);

  // Ajout rayon
  const handleAddSection = async () => {
    if (!storeId || !newType) return;

    if (sections.length >= 6) {
      alert("Votre magasin ne peut pas avoir plus de 6 rayons !");
      return;
    }

    try {
      await createSection(storeId, newType);
      const updatedSections = await fetchSections(storeId);
      const filtered = updatedSections.filter((s) => s.storeId === storeId);
      setSections(filtered);
      setShowPopup(false);
      setNewType("");
    } catch (err) {
      alert(err.message || "Erreur lors de l'ajout du rayon");
    }
  };

  return (
    <div className="magasin-container">
      <div className="magasin">
        <div className="nav-magasin">
          <h1 className="titre-magasin">{storeName ?? "Mon Magasin"}</h1>
          <p className="argent-magasin">
            {argent !== null ? argent + " $" : "Chargement..."}
          </p>

          <button
            onClick={() => setShowLeaderboard(!showLeaderboard)}
            className="toggle-button"
          >
            {showLeaderboard ? "Masquer le classement" : "Afficher le classement"}
          </button>
        </div>

        <div className="rayons">
          {sections.length === 0 ? (
            <p>Chargement des rayons...</p>
          ) : (
            sections.map((section) =>
              section.type && section.level ? (
                <Rayon
                  key={section.id}
                  id={section.id}
                  type={section.type}
                  initialLevel={section.level}
                  upgradePrice={section.upgradePrice}
                />
              ) : null
            )
          )}

          {sections.length < 6 && (
            <div className="ajout-rayon-container">
              <button
                onClick={() => setShowPopup(true)}
                title="Ajouter un rayon"
              >
                ➕
              </button>

              {showPopup && (
                <div className="popup-overlay">
                  <div className="popup-content">
                    <h3>Ajouter un nouveau rayon</h3>
                    <select
                      value={newType}
                      onChange={(e) => setNewType(e.target.value)}
                    >
                      <option value="">Choisir un type</option>
                      <option value="Action">Action</option>
                      <option value="Comédie">Comédie</option>
                      <option value="Horreur">Horreur</option>
                      <option value="SciFi">SciFi</option>
                    </select>
                    <div className="popup-buttons">
                      <button onClick={handleAddSection} disabled={!newType}>
                        Valider
                      </button>
                      <button onClick={() => setShowPopup(false)}>
                        Annuler
                      </button>
                    </div>
                  </div>
                </div>
              )}
            </div>
          )}
        </div>
      </div>

      {showLeaderboard && (
        <div className="leaderboard-container">
          <h2>Classement</h2>
          {leaderboard.length === 0 ? (
            <p>Chargement...</p>
          ) : (
            <ul className="leaderboard-list">
              {leaderboard.map((player, index) => (
                <li
                  key={player.userId}
                  className={player.userId === userId ? "joueur-actuel top-1" : ""}
                >
                  <span>
                    {index + 1}. {player.pseudo}
                  </span>
                  <span>{player.score}</span>
                </li>
              ))}
            </ul>
          )}
        </div>
      )}
    </div>
  );
};

export default Magasin;
