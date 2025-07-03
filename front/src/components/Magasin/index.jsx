import React, { useEffect, useState } from "react";
import {
  fetchStoreByUserId,
  fetchStoreMoney,
  fetchSections,
  createSection,
  fetchLeaderboard,
  fetchWarehouseCapacity,
  upgradeWarehouse,
} from "../../services/api";
import Rayon from "../Rayon/index";
import "../../style/Magasin/style.css";

const Magasin = () => {
  const [argent, setArgent] = useState(null);
  const [storeId, setStoreId] = useState(null);
  const [storeName, setStoreName] = useState(null);
  const [sections, setSections] = useState([]);
  const [userId, setUserId] = useState(null);
  const [warehouseCap, setWarehouseCap] = useState(null);

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

    const refreshWarehouse = () => {
  if (!storeId) return;
  fetchWarehouseCapacity(storeId)
    .then(data => setWarehouseCap(data.capacity))
    .catch(err => console.error("Erreur récupération capacité :", err));
};





    refreshArgent();
    refreshWarehouse();
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

  const handleWarehouseUpgrade = async () => {
    try {
      await upgradeWarehouse(storeId);
      const data = await fetchWarehouseCapacity(storeId);
      setWarehouseCap(data.capacity);
    } catch (err) {
      alert(err.message || "Erreur lors de l'amélioration de l'entrepôt");
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
          <p className="entrepot-capacite">Capacité : {warehouseCap ?? "..."} $</p>
          <button className="upgrade-entrepot-btn" onClick={handleWarehouseUpgrade}>
          Améliorer entrepôt</button>

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
          
            <button class="addBtn" onClick={() => setShowPopup(true)} title="Ajouter un rayon">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus-square-dotted" viewBox="0 0 16 16">
              <path d="M2.5 0q-.25 0-.487.048l.194.98A1.5 1.5 0 0 1 2.5 1h.458V0zm2.292 0h-.917v1h.917zm1.833 0h-.917v1h.917zm1.833 0h-.916v1h.916zm1.834 0h-.917v1h.917zm1.833 0h-.917v1h.917zM13.5 0h-.458v1h.458q.151 0 .293.029l.194-.981A2.5 2.5 0 0 0 13.5 0m2.079 1.11a2.5 2.5 0 0 0-.69-.689l-.556.831q.248.167.415.415l.83-.556zM1.11.421a2.5 2.5 0 0 0-.689.69l.831.556c.11-.164.251-.305.415-.415zM16 2.5q0-.25-.048-.487l-.98.194q.027.141.028.293v.458h1zM.048 2.013A2.5 2.5 0 0 0 0 2.5v.458h1V2.5q0-.151.029-.293zM0 3.875v.917h1v-.917zm16 .917v-.917h-1v.917zM0 5.708v.917h1v-.917zm16 .917v-.917h-1v.917zM0 7.542v.916h1v-.916zm15 .916h1v-.916h-1zM0 9.375v.917h1v-.917zm16 .917v-.917h-1v.917zm-16 .916v.917h1v-.917zm16 .917v-.917h-1v.917zm-16 .917v.458q0 .25.048.487l.98-.194A1.5 1.5 0 0 1 1 13.5v-.458zm16 .458v-.458h-1v.458q0 .151-.029.293l.981.194Q16 13.75 16 13.5M.421 14.89c.183.272.417.506.69.689l.556-.831a1.5 1.5 0 0 1-.415-.415zm14.469.689c.272-.183.506-.417.689-.69l-.831-.556c-.11.164-.251.305-.415.415l.556.83zm-12.877.373Q2.25 16 2.5 16h.458v-1H2.5q-.151 0-.293-.029zM13.5 16q.25 0 .487-.048l-.194-.98A1.5 1.5 0 0 1 13.5 15h-.458v1zm-9.625 0h.917v-1h-.917zm1.833 0h.917v-1h-.917zm1.834-1v1h.916v-1zm1.833 1h.917v-1h-.917zm1.833 0h.917v-1h-.917zM8.5 4.5a.5.5 0 0 0-1 0v3h-3a.5.5 0 0 0 0 1h3v3a.5.5 0 0 0 1 0v-3h3a.5.5 0 0 0 0-1h-3z"/>
            </svg>
          </button>

          

          {showPopup && (
            <div className="popup-overlay">
              <div className="popup-content">
                <h3>Ajouter un nouveau rayon</h3>

                <select value={newType} onChange={(e) => setNewType(e.target.value)}>
                  <option value="">Choisir un type</option>
                  <option value="Action">Action</option>
                  <option value="Comédie">Comédie</option>
                  <option value="Horreur">Horreur</option>
                  <option value="SciFi">SciFi</option>
                </select>

                <div className="popup-buttons">
                  <button onClick={handleAddSection} disabled={!newType}>Valider</button>
                  <button onClick={() => setShowPopup(false)}>Annuler</button>
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
