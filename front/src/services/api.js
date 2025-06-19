const API_BASE_URL = 'https://localhost:7264/api';

export async function registerUser(data) {
  const response = await fetch(`${API_BASE_URL}/User/createWithStore`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });

  if (!response.ok) throw new Error("Erreur lors de l'inscription");
  return await response.json(); // contient l'id (userId)
}

export async function fetchStoreByUserId(userId) {
  const response = await fetch(`${API_BASE_URL}/Store/user/${userId}`);
  if (!response.ok) throw new Error("Magasin introuvable pour cet utilisateur");
  return await response.json(); // contient storeId
}

export async function fetchStoreMoney(storeId) {
  const response = await fetch(`${API_BASE_URL}/Resource/calculate/${storeId}`, {
    method: 'POST',
  });

  if (!response.ok) {
    throw new Error("Erreur lors du calcul de l'argent du magasin");
  }

  const data = await response.json();
  return data.money ?? null; 
}