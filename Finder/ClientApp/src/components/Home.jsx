import React from "react";
import { generateMatrix, getMatrix, findAndMatch, findAndSelectWord } from "../services/finder";
import AlphabetSoup from "./AlphabetSoup";

const Home = () => {
  const [matrix, setMatrix] = React.useState({ words: [], matrix: [] });
  const [word, setWord] = React.useState('')

  React.useEffect(() => {
    loadMatrix();
  }, [])

  const loadMatrix = async () => {
    const data = await getMatrix();
    console.log({ data });
    setMatrix(data);
  }

  const generate = async () => {
    const data = await generateMatrix(15);
    console.log({ data });
    setMatrix(data);
  };

  const handleMatch = async (word) => {
    await findAndMatch(word).catch((err) => console.log(err));
  }

  const handleSelect = async () => {
    const result = await findAndSelectWord(word).catch((err) => console.log(err)).finally(() => setWord(''));
    if (!result) {
      alert('Word not found')
    }
    loadMatrix();
  }

  return (
    <div>
      <h1 className="mb-3">Word Search</h1>
      <div className="input-group input-group-lg mb-3">
        <input
          type="text"
          className="form-control"
          placeholder="Search for..."
          aria-label="Recipient's username with two button addons"
          value={word}
          onChange={e => setWord(e.target.value)}
          onKeyUp={e => e.key === 'Enter' && handleSelect()}
        />
        <button className="btn btn-outline-secondary" type="button" onClick={handleSelect}>
          Match
        </button>
        <button
          className="btn btn-outline-secondary"
          type="button"
          onClick={generate}
        >
          Generate Matrix
        </button>
      </div>
      <div>
        <AlphabetSoup soup={matrix} match={handleMatch} select={handleSelect} />
      </div>
    </div>
  );
};

export default Home;
