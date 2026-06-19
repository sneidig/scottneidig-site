import { useState } from 'react'
import Lightbox from './Lightbox'

// Receives `project` from the API: { title, lead, meansLabel, metaText, liveUrl,
// images: [{file, caption}], points: [{title, body}] }.
// images[0] is the hero; the rest fill the grid. Clicking any opens the lightbox.
function Jambalam({ project }) {
  const [openIndex, setOpenIndex] = useState(null) // null = closed
  const images = project.images

  const showPrev = () => setOpenIndex((i) => (i - 1 + images.length) % images.length)
  const showNext = () => setOpenIndex((i) => (i + 1) % images.length)

  const hero = images[0]
  const gallery = images.slice(1)

  return (
    <section className="project" id="work">
      <p className="section__eyebrow">Featured project</p>
      <h2 className="project__title">{project.title}</h2>

      <button className="project__shot" onClick={() => setOpenIndex(0)} aria-label="Expand homepage screenshot">
        <img src={`/images/${hero.file}`} alt={hero.caption} loading="lazy" />
      </button>

      <div className="project__gallery">
        {gallery.map((s, i) => (
          <button className="shot" key={s.file} onClick={() => setOpenIndex(i + 1)}>
            <img src={`/images/${s.file}`} alt={s.caption} loading="lazy" />
            <figcaption>{s.caption}</figcaption>
          </button>
        ))}
      </div>

      <p className="project__lead">{project.lead}</p>

      <p className="project__meansLabel">{project.meansLabel}</p>
      <ul className="project__points">
        {project.points.map((p) => (
          <li className="project__point" key={p.title}>
            <strong>{p.title}.</strong> {p.body}
          </li>
        ))}
      </ul>

      <p className="project__meta">
        {project.metaText} ·{' '}
        <a href={project.liveUrl} target="_blank" rel="noreferrer">live at jambalam.com</a>
      </p>

      <Lightbox
        images={images}
        index={openIndex}
        onClose={() => setOpenIndex(null)}
        onPrev={showPrev}
        onNext={showNext}
      />
    </section>
  )
}

export default Jambalam
