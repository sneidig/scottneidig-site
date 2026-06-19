// App fetches the page content from the ASP.NET Core API, then renders the sections.
import { useEffect, useState } from 'react'
import Hero from './components/Hero'
import Jambalam from './components/Jambalam'
import Skills from './components/Skills'
import Contact from './components/Contact'
import './App.css'

function App() {
  const [content, setContent] = useState(null)
  const [error, setError] = useState(false)

  // Load content once when the app mounts.
  useEffect(() => {
    fetch('/api/content')
      .then((res) => {
        if (!res.ok) throw new Error('Failed to load')
        return res.json()
      })
      .then(setContent)
      .catch(() => setError(true))
  }, [])

  return (
    <main className="page">
      <Hero />

      {error && <p className="loadnote">Couldn’t load content — is the API running?</p>}
      {!content && !error && <p className="loadnote">Loading…</p>}

      {content && (
        <>
          <Jambalam project={content.project} />
          <Skills skillGroups={content.skillGroups} />
        </>
      )}

      <Contact />

      <footer className="footer">
        © {new Date().getFullYear()} Scott Neidig · Built with React + ASP.NET Core
      </footer>
    </main>
  )
}

export default App
