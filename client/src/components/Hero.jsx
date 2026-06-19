// Hero — the first thing a visitor sees. They already have your resume; this just
// confirms "yes, this is a strong senior engineer."
function Hero() {
  return (
    <header className="hero">
      <p className="hero__eyebrow">Senior .NET Engineer · 20+ years</p>
      <h1 className="hero__title">Scott Neidig</h1>
      <p className="hero__lead">
        I architect and extend complex .NET platforms — including custom nopCommerce
        plugin architectures built on solid OOP and SOLID design. I take systems from
        design to production, with AI integrated into real features and full-stack
        delivery when it&rsquo;s needed. Most recently I built and operate{' '}
        <a href="https://www.jambalam.com" target="_blank" rel="noreferrer">Jambalam</a>,
        an AI-powered platform on a custom nopCommerce plugin, solo.
      </p>
      <a className="hero__cta" href="#contact">Get in touch</a>
    </header>
  )
}

export default Hero
