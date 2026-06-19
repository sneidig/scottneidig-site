import { useState } from 'react'

// Contact form — POSTs to the ASP.NET Core API, which saves the message and
// (when SMTP is configured) emails it. Your address stays off the public page.
function Contact() {
  const [form, setForm] = useState({ name: '', email: '', message: '', website: '' })
  const [status, setStatus] = useState('idle') // idle | sending | sent | error
  const [error, setError] = useState('')

  // One handler updates whichever field changed (by its `name`).
  const update = (e) => setForm({ ...form, [e.target.name]: e.target.value })

  async function handleSubmit(e) {
    e.preventDefault()
    setStatus('sending')
    setError('')

    try {
      const res = await fetch('/api/contact', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(form),
      })
      const data = await res.json().catch(() => ({}))

      if (!res.ok) throw new Error(data.error || 'Something went wrong. Please try again.')

      setStatus('sent')
      setForm({ name: '', email: '', message: '', website: '' })
    } catch (err) {
      setStatus('error')
      setError(err.message)
    }
  }

  return (
    <section className="contact" id="contact">
      <p className="section__eyebrow">Contact</p>
      <h2 className="contact__title">Let’s talk.</h2>
      <p className="contact__lead">
        If you’re hiring for senior .NET, full-stack, or AI engineering work, I’d be
        glad to connect.
      </p>

      {status === 'sent' ? (
        <p className="contact__success">Thanks — your message is on its way. I’ll be in touch.</p>
      ) : (
        <form className="contact__form" onSubmit={handleSubmit}>
          <input
            name="name" placeholder="Your name" value={form.name}
            onChange={update} required
          />
          <input
            name="email" type="email" placeholder="Your email" value={form.email}
            onChange={update} required
          />
          <textarea
            name="message" placeholder="Your message" rows="5" value={form.message}
            onChange={update} required
          />

          {/* Honeypot: hidden from people, tempting to bots. Real users leave it blank. */}
          <input
            name="website" value={form.website} onChange={update}
            className="contact__hp" tabIndex="-1" autoComplete="off" aria-hidden="true"
          />

          <button className="contact__cta" type="submit" disabled={status === 'sending'}>
            {status === 'sending' ? 'Sending…' : 'Send message'}
          </button>

          {status === 'error' && <p className="contact__error">{error}</p>}
        </form>
      )}

      <p className="contact__alt">
        Prefer LinkedIn?{' '}
        <a href="https://www.linkedin.com/in/scottneidig/" target="_blank" rel="noreferrer">
          Connect with me there
        </a>.
      </p>
    </section>
  )
}

export default Contact
