const API_BASE_URL = 'http://localhost:4000/api' // à adapter selon ton backend

export async function registerUser(data) {
  const response = await fetch(`${API_BASE_URL}/register`, {
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
