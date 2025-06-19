import React, { useEffect, useState } from "react";
import { fetchStore, fetchSections } from "../../services/api";
import Rayon from '../../components/Rayon';

export default function RayonPages() {
  const [stores, setStores] = useState([]);
  const [selectedStoreId, setSelectedStoreId] = useState("");
  const [rayons, setRayons] = useState([]);
  const [loadingRayons, setLoadingRayons] = useState(false);

  // Charger les magasins au chargement
  useEffect(() => {
    fetchStore()
      .then(data => {
        setStores(data);
        if (data.length > 0) setSelectedStoreId(data[0].id);
      })
      .catch(() => alert("Erreur lors du chargement des magasins"));
  }, []);

  // Charger les rayons quand un magasin est sélectionné
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

      <label>
        Sélectionnez un magasin :
        <select
          value={selectedStoreId}
          onChange={(e) => setSelectedStoreId(e.target.value)}
          style={{ marginLeft: "1rem" }}
        >
          {stores.map(store => (
            <option key={store.id} value={store.id}>
              {store.storeName || store.name || store.id}
            </option>
          ))}
        </select>
      </label>

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
