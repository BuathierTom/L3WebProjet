import React, { useEffect, useState } from "react";
import { fetchSections } from "../../services/api";
import Rayon from '../../components/Rayon';

export default function RayonPages() {
  const [selectedStoreId, setSelectedStoreId] = useState("");
  const [rayons, setRayons] = useState([]);
  const [loadingRayons, setLoadingRayons] = useState(false);

  useEffect(() => {
    // Récupérer storeId depuis localStorage directement
    const storeIdLS = localStorage.getItem("storeId");
    if (!storeIdLS) {
      alert("Aucun magasin sélectionné (storeId manquant dans localStorage)");
      return;
    }
    setSelectedStoreId(storeIdLS);
  }, []);

  useEffect(() => {
    if (!selectedStoreId) return;

    setLoadingRayons(true);
    fetchSections(selectedStoreId)
      .then(data => setRayons(data))
      .catch(() => alert("Erreur lors du chargement des rayons"))
      .finally(() => setLoadingRayons(false));
  }, [selectedStoreId]);

  return (
    <div style={{ padding: "1rem" }}>
      <h1>Gestion des Rayons</h1>

      {/* Suppression de la liste déroulante */}

      <div style={{ marginTop: "2rem" }}>
        {loadingRayons ? (
          <p>Chargement des rayons...</p>
        ) : rayons.length === 0 ? (
          <p>Aucun rayon trouvé pour ce magasin.</p>
        ) : (
          <div style={{ display: "flex", gap: "1rem", flexWrap: "wrap" }}>
            {rayons.map(rayon => (
              <Rayon
                key={rayon.id}
                id={rayon.id}
                type={rayon.type.toLowerCase()}
                initialLevel={rayon.level}
                storeId={selectedStoreId}
              />
            ))}
          </div>
        )}
      </div>
    </div>
  );
}
