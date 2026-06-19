// Receives `skillGroups` from the API: [{ label, skills: ["C#", ...] }]
function Skills({ skillGroups }) {
  return (
    <section className="skills" id="skills">
      <p className="section__eyebrow">Skills</p>
      <div className="skills__grid">
        {skillGroups.map((g) => (
          <div className="skills__group" key={g.label}>
            <h3 className="skills__label">{g.label}</h3>
            <ul className="skills__list">
              {g.skills.map((name) => (
                <li key={name}>{name}</li>
              ))}
            </ul>
          </div>
        ))}
      </div>
    </section>
  )
}

export default Skills
