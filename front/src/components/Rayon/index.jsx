import React, { useState } from "react";
import '../../style/Rayon/style.css';
import { upgradeRayon } from "../../services/api";

import actionImg from "../../assets/images/rayon-action.png";
import horreurImg from "../../assets/images/rayon-horreur.png";
import comedieImg from "../../assets/images/rayon-comedie.png";
import scifiImg from "../../assets/images/rayon-scifi.png";

const rayonImages = {
  action: actionImg,
  horreur: horreurImg,
  comedie: comedieImg,
  scifi: scifiImg,
};

const Rayon = ({ id, type = "action", initialLevel = 1 }) => {
  const [level, setLevel] = useState(initialLevel);
  const [loading, setLoading] = useState(false);
  const image = rayonImages[type] || actionImg;
  const label = type.charAt(0).toUpperCase() + type.slice(1);

  const handleUpgrade = async () => {
    const storeId = localStorage.getItem("storeId");
    if (!storeId) {
      alert("❌ storeId manquant dans le localStorage");
      return;
    }

    try {
      setLoading(true);
      await upgradeRayon(id, storeId); // Envoie la requête POST
      setLevel(prev => prev + 1);      // Met à jour immédiatement le niveau
      alert(`✅ Upgrade réussi ! Nouveau niveau : ${level + 1}`);
    } catch (err) {
      alert(err.message || "Erreur pendant l'upgrade");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="rayon">
      <img src={image} alt={`Rayon ${label}`} className="rayon-image" />
      <p className="rayon-label">{label} - Niveau {level}</p>
      <button className="rayon-button" onClick={handleUpgrade} disabled={loading}>
        {loading ? "..." : "Upgrade"}
      </button>
    </div>
  );
};

export default Rayon;
