import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import '../../style/Formulaire/formulaire.css'
import { registerUser } from '../../services/api' // à créer

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
      console.log('Envoi des données au backend...')
      const result = await registerUser({ pseudo, magasin })
      console.log('Inscription réussie:', result)
      navigate('/dashboard')  // ← redirection ici
    } catch (error) {
      alert(error.message || "Erreur lors de l'inscription")
    } finally {
      setLoading(false)
    }
  }

  return (
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
  )
}
