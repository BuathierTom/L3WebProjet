import React, { useEffect, useState } from "react";
import { fetchStoreMoney } from "../../services/api"; // adapter le chemin
import Rayon from "../Rayon/index";
import "../../style/Magasin/style.css"

const Magasin = () => {
  const [argent, setArgent] = useState(null);
  const storeId = localStorage.getItem("storeId");
  const [storeName, setStoreName] = useState(null);

  // 💰 Fonction de refresh d'argent
  const refreshArgent = () => {
  if (!storeId) return;
  fetchStoreMoney(storeId)
    .then(setArgent)
    .then((data) => setStoreName(data.name))
    .catch((err) => console.error("Erreur refresh argent :", err));
};

  useEffect(() => {
    refreshArgent();

    const interval = setInterval(refreshArgent, 3000);
    return () => clearInterval(interval); 
  }, [storeId]);
  
  return (
    <div className="magasin">
      <div className="nav-magasin">
        <h1 className="titre-magasin">{storeName ?? "Mon Magasin"}</h1>
        <p className="argent-magasin"> {argent !== null ? argent + " $" : "Chargement..."}</p>
        <p className="id-magasin"> <strong>{storeId ?? "Chargement..."}</strong></p>
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
