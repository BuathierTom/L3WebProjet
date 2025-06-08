import { Route, Routes } from 'react-router';
import Home from './pages/Home'
import Magasin from './components/Magasin';

export default function App() {
  return(
    <Routes>
      <Route path="/" element={<Home />} />
    </Routes>
  )
  
}


