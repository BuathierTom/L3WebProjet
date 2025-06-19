import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import '../../style/Formulaire/formulaire.css'
import { registerUser } from '../../services/api'

export default function Formulaire() {
  const [pseudo, setPseudo] = useState('')
  const [magasin, setMagasin] = useState('')
  const [loading, setLoading] = useState(false)
  const navigate = useNavigate()

  const handleSubmit = async (e) => {
    e.preventDefault()

    if (!pseudo.trim() || !magasin.trim()) {
      alert('Veuillez entrer un pseudo et un nom de magasin')
      return
    }

    setLoading(true)
    try {
      const result = await registerUser({ pseudo, storeName: magasin })

      console.log('Inscription réussie:', result) // Voir la structure exacte de la réponse

      // Récupération du storeId (à adapter selon la réponse)
      const storeId = result.store?.id || result.id

      localStorage.setItem('userId', result.userId)
      localStorage.setItem('storeId', storeId)

      navigate('/home')
    } catch (error) {
      alert(error.message || "Erreur lors de l'inscription")
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="inscription-background">
      <div className="form-wrapper">
        <h2 className="form-title">Inscription</h2>

        <form className="form-container" onSubmit={handleSubmit}>
          <input
            type="text"
            placeholder="Entrez votre pseudo"
            value={pseudo}
            onChange={(e) => setPseudo(e.target.value)}
            disabled={loading}
          />
          <input
            type="text"
            placeholder="Entrez le nom du magasin"
            value={magasin}
            onChange={(e) => setMagasin(e.target.value)}
            disabled={loading}
          />
          <button type="submit" disabled={loading}>
            {loading ? 'Chargement...' : 'Valider'}
          </button>
        </form>
      </div>
    </div>
  )
}
