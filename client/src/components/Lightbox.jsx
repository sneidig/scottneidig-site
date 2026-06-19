import { useEffect } from 'react'

// Lightbox — a full-screen overlay to view one image large, with prev/next arrows.
// Props:
//   images: [{ file, caption }]   the full set to page through
//   index:  number                which one is showing (null = closed)
//   onClose, onPrev, onNext       callbacks from the parent
function Lightbox({ images, index, onClose, onPrev, onNext }) {
  // Wire up keyboard: Esc closes, arrows navigate. Runs whenever `index` changes.
  useEffect(() => {
    if (index === null) return

    function handleKey(e) {
      if (e.key === 'Escape') onClose()
      if (e.key === 'ArrowLeft') onPrev()
      if (e.key === 'ArrowRight') onNext()
    }

    window.addEventListener('keydown', handleKey)
    document.body.style.overflow = 'hidden' // stop the page scrolling behind the modal

    // Cleanup when it closes or index changes
    return () => {
      window.removeEventListener('keydown', handleKey)
      document.body.style.overflow = ''
    }
  }, [index, onClose, onPrev, onNext])

  if (index === null) return null
  const current = images[index]

  return (
    <div className="lb" onClick={onClose} role="dialog" aria-modal="true">
      <button className="lb__close" onClick={onClose} aria-label="Close">×</button>

      <button
        className="lb__arrow lb__arrow--prev"
        onClick={(e) => { e.stopPropagation(); onPrev() }}
        aria-label="Previous image"
      >‹</button>

      {/* stopPropagation so clicking the image itself doesn't close the modal */}
      <figure className="lb__figure" onClick={(e) => e.stopPropagation()}>
        <img src={`/images/${current.file}`} alt={current.caption} />
        <figcaption>{current.caption}</figcaption>
      </figure>

      <button
        className="lb__arrow lb__arrow--next"
        onClick={(e) => { e.stopPropagation(); onNext() }}
        aria-label="Next image"
      >›</button>
    </div>
  )
}

export default Lightbox
