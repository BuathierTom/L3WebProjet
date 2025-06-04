import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import '../../style/Formulaire/formulaire.css'

export default function Formulaire() {
  const [pseudo, setPseudo] = useState('')
  const [magasin, setMagasin] = useState('')
  const navigate = useNavigate()

  const handleSubmit = (e) => {
    e.preventDefault()

    if (!pseudo.trim() || !magasin.trim()) {
      alert('Veuillez entrer un pseudo et un nom de magasin')
      return
    }

    console.log('Pseudo :', pseudo)
    console.log('Magasin :', magasin)

    navigate('/')
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
        />
        <input
          type="text"
          placeholder="Entrez le nom du magasin"
          value={magasin}
          onChange={(e) => setMagasin(e.target.value)}
        />
        <button type="submit">Valider</button>
      </form>
    </div>
  )
}
