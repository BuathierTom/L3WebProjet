export async function fetchStore(storeId) {
  const response = await fetch(`../data/store.json`);
  if (!response.ok) {
    throw new Error("Erreur lors du chargement du magasin");
  }
  return await response.json();
}
