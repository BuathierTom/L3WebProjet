import React, { useEffect, useState } from "react";
import Rayon from "../Rayon/index";
import { fetchStore } from "../../services/services";

const Magasin = () => {
  const [buildings, setBuildings] = useState([]);
  const [resources, setResources] = useState(null);
  const storeId = "1234"; // à remplacer par un vrai ID (ex: auth/localStorage)

  useEffect(() => {
  const load = async () => {
    const response = await fetch("../../data/store.json");
    const data = await response.json();
    setResources(data.resources);
    setBuildings(data.buildings);
  };

  load();
}, []);

  return (
    <div style={{ padding: "1rem" }}>
      {resources && (
        <div style={{ marginBottom: "1rem", fontFamily: "monospace" }}>
          💰 Argent : {resources.money} | 📦 Stock : {resources.stock} | ✨ Popularité : {resources.popularity}
        </div>
      )}

      <div style={{ display: "flex", gap: "2rem", flexWrap: "wrap" }}>
        {buildings.map((b) => (
          <Rayon key={b.id} type={b.type} />
        ))}
      </div>
    </div>
  );
};

export default Magasin;
