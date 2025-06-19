const API_BASE_URL = 'https://localhost:7264/api';

export async function registerUser(data) {
  const response = await fetch(`${API_BASE_URL}/User/createWithStore`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    const errorData = await response.json();
    throw new Error(errorData.message || "Erreur lors de l'inscription");
  }

  return response.json();
}

export async function fetchStore() {
  const response = await fetch(`${API_BASE_URL}/Store`);
  if (!response.ok) throw new Error("Impossible de charger les magasins");
  return response.json();
}

export async function upgradeRayon(sectionId, storeId) {
  const response = await fetch(`${API_BASE_URL}/Section/upgrade/${sectionId}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ storeId }),
  });

  if (!response.ok) {
    let errorMessage = "❌ Pas assez d'argent pour upgrader";
    try {
      const errorData = await response.json();
      if (errorData.message) errorMessage = errorData.message;
    } catch {}
    throw new Error(errorMessage);
  }

  // ✅ ATTENTION ici : ne pas parser en JSON
  const text = await response.text();
  console.log("Réponse brute :", text);

  // Retourner juste une confirmation ou l’ancien niveau +1 si besoin
  return { success: true }; // ou retourne `text` si tu veux l'afficher
}

export async function fetchSections(storeId) {
  const response = await fetch(`${API_BASE_URL}/section?storeId=${storeId}`);
  if (!response.ok) throw new Error("Impossible de charger les sections");
  return response.json();
}