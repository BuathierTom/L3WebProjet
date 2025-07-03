import React, { useState } from "react";
import '../../style/Rayon/style.css';
import { fetchSectionById, upgradeRayon } from "../../services/api";

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

const Rayon = ({ id, type, initialLevel = 1, upgradePrice }) => {
  const [level, setLevel] = useState(initialLevel);
  const [price, setPrice] = useState(upgradePrice);
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState(null);       // texte popup
  const [messageType, setMessageType] = useState(null); // "success" ou "error"
  
  const normalize = (str) =>
  str
    .normalize("NFD")                 // décompose les accents
    .replace(/[\u0300-\u036f]/g, "") // supprime les accents
    .toLowerCase();

  const normalizedType = normalize(type || "action");
  const image = rayonImages[normalizedType] || actionImg;
  const label = normalizedType.charAt(0).toUpperCase() + normalizedType.slice(1);


  const showMessage = (msg, type) => {
    setMessage(msg);
    setMessageType(type);
    setTimeout(() => {
      setMessage(null);
      setMessageType(null);
    }, 2000);
  };

  const handleUpgrade = async () => {
    const storeId = localStorage.getItem("storeId");
    if (!storeId) {
      showMessage(" storeId manquant dans le localStorage", "error");
      return;
    }

    try {
      setLoading(true);
      await upgradeRayon(id, storeId);
      const updatedSection = await fetchSectionById(id);

      setLevel(prev => prev + 1);
      setPrice(updatedSection.upgradePrice)
      console.log(" Section mise à jour :", updatedSection);
      showMessage(` Upgrade réussi ! Nouveau niveau : ${level + 1}`, "success");
    } catch (err) {
      if (err.message && err.message.toLowerCase().includes("argent")) {
        showMessage(" Pas assez d'argent pour upgrader", "error");
      } else {
        showMessage(err.message || "Erreur pendant l'upgrade", "error");
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="rayon">
      <img src={image} alt={`Rayon ${label}`} className="rayon-image" />
      <p className="rayon-label">{label} - Niveau {level}</p>
      <button className="rayon-button" onClick={handleUpgrade} disabled={loading}>
        {loading ? "..." : `Upgrade (${price} $)`}
      </button>

      {message && (
        <div className={`popup-message ${messageType === "success" ? "popup-success" : "popup-error"}`}>
          {message}
        </div>
      )}
    </div>
  );
};

export default Rayon;
