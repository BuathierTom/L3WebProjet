import React from "react";
import Rayon from "../../components/Rayon"

export default function Home() {
    return (
    <div style={{ display: "flex", gap: "2rem", flexWrap: "wrap" }}>
      <Rayon type="action" onClick={() => console.log("Action cliqué")} />
      <Rayon type="horreur" onClick={() => console.log("Horreur cliqué")} />
      <Rayon type="comedie" onClick={() => console.log("Comédie cliqué")} />
      <Rayon type="scifi" onClick={() => console.log("Sci-Fi cliqué")} />
    </div>
  );
};

