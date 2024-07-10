import React, {useEffect, useState} from "react";
import artistIcon from "../../assets/images/artistIcon.jpg";
import {IArtist} from "../../types/global.typing";
import axios from "axios";
import {Link} from "react-router-dom";

import "./artists.scss"

const Artists: React.FC = () => {
    const [artists, setArtists] = useState<IArtist[]>([]);

    const fetchArtistsList = async () => {
        try {
            const response = await axios.get<IArtist[]>("https://localhost:7130/api/Artists");
            setArtists(response.data);
        } catch (error){
            alert("Server Error")
        }
    }

    useEffect(() => {
        fetchArtistsList();
    }, [])

    return(
        <div className="cardsWrap">
            <h1>Artists</h1>
            <div className="cardsWrapInner">
                {artists.map((artist) => (
                    <div className="card" key={artist.artistId}>
                        <Link to={'/albums/' + artist.artistId} style={{ textDecoration: "none" }} >
                            <div className="cardImage">
                                <img src={artistIcon} alt="Artist" />
                            </div>
                            <div className="cardContent">
                                <h3>{artist.artistName}</h3>
                            </div>
                        </Link>
                    </div>
                ))}
            </div>
        </div>
    )
}

export default Artists