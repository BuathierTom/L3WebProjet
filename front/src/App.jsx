import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import { Routes, Route } from 'react-router-dom'
import Home from './pages/Home'
import Inscription from './pages/Inscription/inscription'

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<Inscription />} />
      <Route path="/home" element={<Home />} />
    </Routes>
  )
}


