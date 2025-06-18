import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  test: {
    globals: true,          // ← obligatoire pour beforeAll, test, expect...
    environment: 'jsdom',   // ← nécessaire pour tester du React dans un navigateur simulé
  },
})

