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
  const [message, setMessage] = useState(null);       // texte popup
  const [messageType, setMessageType] = useState(null); // "success" ou "error"
  const image = rayonImages[type] || actionImg;
  const label = type.charAt(0).toUpperCase() + type.slice(1);

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
      showMessage("❌ storeId manquant dans le localStorage", "error");
      return;
    }

    try {
      setLoading(true);
      await upgradeRayon(id, storeId);
      setLevel(prev => prev + 1);
      showMessage(`✅ Upgrade réussi ! Nouveau niveau : ${level + 1}`, "success");
    } catch (err) {
      if (err.message && err.message.toLowerCase().includes("argent")) {
        showMessage("❌ Pas assez d'argent pour upgrader", "error");
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
        {loading ? "..." : "Upgrade"}
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
