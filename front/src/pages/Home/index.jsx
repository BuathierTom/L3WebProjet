import React from "react";
import Rayon from "../../components/Rayon"
import Magasin from "../../components/Magasin";

export default function Home() {
    return (
      
    <div class="listRayon">
      <Magasin/>
      <Rayon type="action" onClick={() => console.log("Action cliqué")} />
      <Rayon type="horreur" onClick={() => console.log("Horreur cliqué")} />
      <Rayon type="comedie" onClick={() => console.log("Comédie cliqué")} />
      <Rayon type="scifi" onClick={() => console.log("Sci-Fi cliqué")} />
    </div>
  );
};

