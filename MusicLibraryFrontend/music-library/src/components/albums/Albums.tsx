import React, {useEffect, useState} from "react";
import {Link, useParams} from "react-router-dom";
import albumIcon from "../../assets/images/albumIcon.jpg";
import {IAlbum} from "../../types/global.typing";
import axios from "axios";

import "./albums.scss"

const Albums: React.FC = () => {
    const [albums, setAlbums] = useState<IAlbum[]>([]);
    const { artistId } = useParams<{ artistId: string }>();
    const fetchAlbumsList = async (artistId: string) => {
        try {
            const response = await axios.get<IAlbum[]>(`https://localhost:7130/api/Albums/artist/${artistId}`);
            setAlbums(response.data);
        } catch (error){
            alert("Server Error")
        }
    }

    useEffect(() => {
        if (artistId) {
            fetchAlbumsList(artistId);
        }
    }, [artistId]);

    return(
        <div className="cardsWrap">
            <h1>Albums</h1>
            <div className="cardsWrapInner">
                {albums.map((album) => (
                    <div className="card" key={album.albumId}>
                        <Link to={'/songs/' + album.albumId} style={{ textDecoration: "none" }}>
                            <div className="cardImage">
                                <img src={albumIcon} alt="Album" />
                            </div>
                            <div className="cardContent">
                                <h3>{album.albumTitle}</h3>
                            </div>
                        </Link>
                    </div>
                ))}
            </div>
        </div>
    )
}

export default Albums