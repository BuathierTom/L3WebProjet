import React, { useEffect, useState } from "react";
import { fetchStoreByUserId, fetchStoreMoney, fetchSections } from "../../services/api";
import Rayon from "../Rayon/index";
import "../../style/Magasin/style.css";
import { createSection } from "../../services/api";


const Magasin = () => {
  const [argent, setArgent] = useState(null);
  const [storeId, setStoreId] = useState(null);
  const [storeName, setStoreName] = useState(null);
  const [sections, setSections] = useState([]);
  const [showPopup, setShowPopup] = useState(false);
  const [newType, setNewType] = useState("");



  useEffect(() => {
    let userId = localStorage.getItem("userId");
    console.log("userId récupéré :", userId);

    if (!userId) {
      userId = "3645a00c-680d-4dc0-895e-be7ca8605ecc"; // userId test
      localStorage.setItem("userId", userId);
      console.log("userId mis en localStorage pour test :", userId);
    }

    fetchStoreByUserId(userId)
      .then(stores => {
        console.log("Magasins reçus :", stores);

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
      .catch(err => console.error("Erreur récupération magasin lié à l'utilisateur :", err));
  }, []);

  useEffect(() => {
    if (!storeId) return;

    const refreshArgent = () => {
      fetchStoreMoney(storeId)
        .then(data => {
          console.log("Argent reçu :", data);
          setArgent(data.money ?? data);
        })
        .catch(err => console.error("Erreur refresh argent :", err));
    };
    

    fetchSections(storeId)
      .then(sectionsData => {
        console.log("Sections reçues pour storeId", storeId, ":", sectionsData);
        // Filtrer les sections pour ne garder que celles du magasin courant
        const filteredSections = sectionsData.filter(section => section.storeId === storeId);
        setSections(filteredSections);
      })
      .catch(err => console.error("Erreur chargement sections :", err));

    refreshArgent();
    const interval = setInterval(refreshArgent, 3000);
    return () => clearInterval(interval);
  }, [storeId]);


  const handleAddSection = async () => {
    if (!storeId || !newType) return;

    if (sections.length >= 6) {
    alert("Votre magasin ne peut pas avoir plus de 6 rayons !");
    return;
  }


    try {
      await createSection(storeId, newType);
      const updatedSections = await fetchSections(storeId);
      const filtered = updatedSections.filter((section) => section.storeId === storeId);
      setSections(filtered);
      setShowPopup(false);
      setNewType("");
    } catch (err) {
      alert(err.message || "Erreur lors de l'ajout du rayon");
    }
  };





  return (
    <div className="magasin">
      <div className="nav-magasin">
        <h1 className="titre-magasin">{storeName ?? "Mon Magasin"}</h1>
        <p className="argent-magasin">{argent !== null ? argent + " $" : "Chargement..."}</p>
        <p className="id-magasin"><strong>{storeId ?? "Chargement..."}</strong></p>
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
          
            <button onClick={() => setShowPopup(true)} title="Ajouter un rayon">
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
  );
};

export default Magasin;
