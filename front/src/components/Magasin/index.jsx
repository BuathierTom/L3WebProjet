import React, { useEffect, useState } from "react";
import { fetchStoreByUserId, fetchStoreMoney, fetchSections } from "../../services/api";
import Rayon from "../Rayon/index";
import "../../style/Magasin/style.css";

const Magasin = () => {
  const [argent, setArgent] = useState(null);
  const [storeId, setStoreId] = useState(null);
  const [storeName, setStoreName] = useState(null);
  const [sections, setSections] = useState([]);

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
          sections.map(section => (
            <Rayon
              key={section.id}
              id={section.id}
              type={section.type.toLowerCase()}
              initialLevel={section.level}
            />
          ))
        )}
      </div>
    </div>
  );
};

export default Magasin;
