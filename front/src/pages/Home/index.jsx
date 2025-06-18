import { Link } from 'react-router-dom'
import Magasin from "../../components/Magasin/index";
import React from "react";

export default function Home() {
  return (
    <div>
      <h1>Page principale</h1>
      <Link to="/inscription">Aller à l'inscription</Link>
      <Magasin/>
    </div>
  )
}