import { Link } from 'react-router-dom'

export default function Home() {
  return (
    <div>
      <h1>Page principale</h1>
      <Link to="/inscription">Aller à l'inscription</Link>
    </div>
  )
}

