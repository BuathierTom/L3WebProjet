import React, { useEffect, useState } from "react";

const Magasin = () => {
  const [storeId, setStoreId] = useState(null);

  useEffect(() => {
    // 🔁 On récupère le storeId stocké lors de l'inscription
    const storedId = localStorage.getItem("storeId");
    if (storedId) {
      setStoreId(storedId);
    } else {
      console.warn("Aucun storeId trouvé dans le localStorage.");
    }
  }, []);

  return (
    <div style={{ padding: "1rem", fontFamily: "monospace" }}>
      <h2>🏪 Magasin</h2>
      {storeId ? (
        <p>🆔 ID du magasin : <strong>{storeId}</strong></p>
      ) : (
        <p>⏳ Chargement de l’ID du magasin...</p>
      )}
    </div>
  );
};

export default Magasin;
