import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  // During `npm run dev`, forward /api calls to the ASP.NET Core API on :5073.
  // This means the frontend code just calls "/api/..." in both dev and production.
  server: {
    proxy: {
      '/api': 'http://localhost:5073',
    },
  },
})
