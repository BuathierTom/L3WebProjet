import { describe, it, expect, beforeAll, afterAll } from 'vitest'
import { render, screen, waitFor } from '@testing-library/react'
import Magasin from '../components/Magasin' // adapte ce chemin selon ton projet

// Mock de fetch pour simuler l'API back
beforeAll(() => {
  global.fetch = vi.fn((url) => {
    if (url.includes('../data/store.json')) {
      return Promise.resolve({
        ok: true,
        json: async () => ({
          storeId: 'test-store',
          resources: {
            money: 500,
            stock: 24,
            popularity: 67,
          },
          buildings: [
            { id: '1', type: 'action' },
            { id: '2', type: 'horreur' },
            { id: '3', type: 'comedie' },
            { id: '4', type: 'scifi' },
          ],
        }),
      })
    }

    return Promise.reject(new Error('URL non reconnue'))
  })
})

afterAll(() => {
  vi.restoreAllMocks()
})

describe('Magasin', () => {
  it('affiche les ressources et les rayons du magasin', async () => {
    render(<Magasin />)

    // Vérifie l'affichage des ressources
    await waitFor(() => {
      expect(screen.getByText((txt) => txt.includes('Argent'))).to.exist
      expect(screen.getByText((txt) => txt.includes('Stock'))).to.exist
      expect(screen.getByText((txt) => txt.includes('Popularité'))).to.exist
    })

    // Vérifie les rayons
    expect(screen.getByText(/Action/i)).to.exist
    expect(screen.getByText(/Horreur/i)).to.exist
    expect(screen.getByText(/Comedie/i)).to.exist
    expect(screen.getByText(/Scifi/i)).to.exist
  })
})
