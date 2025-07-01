import React, { useEffect, useState } from "react";
import { fetchStore, fetchStoreMoney } from "../../services/api"; // fetchStores pour récupérer la liste des magasins
import Rayon from "../Rayon/index";
import "../../style/Magasin/style.css";

const Magasin = () => {
  const [argent, setArgent] = useState(null);
  const [storeId, setStoreId] = useState(null);
  const [storeName, setStoreName] = useState(null);

  // Charger la liste des magasins et prendre le premier storeId valide
  useEffect(() => {
    fetchStore()
      .then(stores => {
        if (stores.length > 0) {
          setStoreId(stores[0].id);
          setStoreName(stores[0].name);
          localStorage.setItem("storeId", stores[0].id);
        }
      })
      .catch(err => console.error("Erreur chargement magasins", err));
  }, []);

  // Refresh argent
  useEffect(() => {
    if (!storeId) return;

    const refreshArgent = () => {
      fetchStoreMoney(storeId)
        .then(data => setArgent(data.money ?? data)) // selon ce que retourne ton API
        .catch(err => console.error("Erreur refresh argent :", err));
    };

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
        <Rayon className="div-rayon" type="action" />
        <Rayon className="div-rayon" type="horreur" />
        <Rayon className="div-rayon" type="comedie" />
        <Rayon className="div-rayon" type="scifi" />
      </div>
    </div>
  );
};

export default Magasin;
