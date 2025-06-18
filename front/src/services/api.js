const API_BASE_URL = 'https://localhost:7264/api'

export async function registerUser(data) {
  const response = await fetch(`${API_BASE_URL}/User/createWithStore`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
  })

  if (!response.ok) {
    const errorData = await response.json()
    throw new Error(errorData.message || 'Erreur lors de l\'inscription')
  }

  return response.json()
}

export async function fetchStore() {
  const response = await fetch(`${API_BASE_URL}/Store`);
  if (!response.ok) throw new Error("Impossible de charger les magasins");
  return await response.json(); // tableau de magasins
}
