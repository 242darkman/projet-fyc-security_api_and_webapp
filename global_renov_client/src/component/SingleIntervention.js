import { useParams } from "react-router-dom";

function SingleIntervention(){
    const { id } = useParams();

    return (
        <div>
            <h1> Intervention { id } </h1>
        </div>
    );
}

export default SingleIntervention;