import actionImg from "../../assets/images/rayon-action.png";
import horreurImg from "../../assets/images/rayon-horreur.png";
import comedieImg from "../../assets/images/rayon-comedie.png";
import scifiImg from "../../assets/images/rayon-scifi.png";

import "../../style/Rayon/style.css"
const rayonImages = {
  action: actionImg,
  horreur: horreurImg,
  comedie: comedieImg,
  scifi: scifiImg,
};

const Rayon = ({ type = "action", onClick }) => {
  const image = rayonImages[type] || actionImg;
  const label = type.charAt(0).toUpperCase() + type.slice(1);

  return (
    <div onClick={onClick} style={{ textAlign: "center", cursor: "pointer" }}>
      <img
        src={image}
        alt={`Rayon ${label}`}
      />
      <p style={{ fontFamily: "monospace", marginTop: "0.5rem" }}>{label}</p>
    </div>
  );
};

export default Rayon;