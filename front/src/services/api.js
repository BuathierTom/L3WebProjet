const API_BASE_URL = 'https://localhost:7264/api';

// Enregistre un utilisateur avec un magasin
export async function registerUser(data) {
  const response = await fetch(`${API_BASE_URL}/User/createWithStore`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });

  if (!response.ok) throw new Error("Erreur lors de l'inscription");
  return await response.json();
}

// Récupère un magasin via un userId
export async function fetchStoreByUserId(userId) {
  const response = await fetch(`${API_BASE_URL}/Store/user/${userId}`);
  if (!response.ok) throw new Error("Magasin introuvable pour cet utilisateur");
  return await response.json();
}

// Calcule l'argent du magasin
export async function fetchStoreMoney(storeId) {
  const response = await fetch(`${API_BASE_URL}/Resource/calculate/${storeId}`, {
    method: 'POST',
  });

  if (!response.ok) {
    const errorData = await response.json();
    throw new Error(errorData.message || "Erreur lors du calcul de l'argent du magasin");
  }

  return await response.json();
}

// Récupère tous les magasins
export async function fetchStore() {
  const response = await fetch(`${API_BASE_URL}/Store`);
  if (!response.ok) throw new Error("Impossible de charger les magasins");
  return await response.json();
}

// Upgrade un rayon (section)
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

  const text = await response.text();
  console.log("Réponse brute :", text);

  return { success: true };
}

// Récupère les sections d’un magasin (avec log)
export async function fetchSections(storeId) {
  console.log("API - fetchSections appelé avec storeId =", storeId);
  const response = await fetch(`${API_BASE_URL}/Section/?storeId=${storeId}`);
  if (!response.ok) throw new Error("Impossible de charger les sections");
  const sections = await response.json();
  console.log("API - sections reçues :", sections);
  return sections;
}

// Récupère une section précise par son ID (optionnel)
export async function fetchSectionById(sectionId) {
  console.log("API - fetchSectionById appelé avec sectionId =", sectionId);
  const response = await fetch(`${API_BASE_URL}/Section/${sectionId}`);
  if (!response.ok) throw new Error("Section introuvable");
  const section = await response.json();
  console.log("API - section reçue :", section);
  return section;
}

// Optionnel : récupère uniquement le montant d'argent du magasin
export async function fetchStoreMoneyAmount(storeId) {
  const response = await fetch(`${API_BASE_URL}/Resource/calculate/${storeId}`, {
    method: 'POST',
  });

  if (!response.ok) {
    throw new Error("Erreur lors du calcul de l'argent du magasin");
  }

  const data = await response.json();
  return data.money ?? null;
}
