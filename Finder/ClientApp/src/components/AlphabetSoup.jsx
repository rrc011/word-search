import React, { useState } from "react";
import clsx from 'clsx';

const AlphabetSoup = ({ soup, match, select }) => {
  const matrix = soup.matrix || [];
  const words = soup.words || [];

  const [isSelecting, setIsSelecting] = useState(false);
  const [selectedLetters, setSelectedLetters] = useState([]);
  const [selectedWords, setSelectedWords] = useState([]);

  // React.useEffect(() => {
  //   let word = [];
  //   for (let i = 0; i < matrix.length; i++) {
  //     for (let j = 0; j < matrix.length; j++) {
  //       const current = matrix[i][j];
  //       if (current.selected) word.push(current);
  //       if (word && words.includes(word.map((letter) => letter.name).join(""))) {
  //         setSelectedWords(array => [...array, word]);
  //         word = [];
  //       }
  //     }
  //   }
  // }, [matrix])

  React.useEffect(() => {
    let foundWords = [];
    matrix.forEach(row => {
      let word = [];
      row.forEach(letter => {
        if (letter.selected) {
          word.push(letter);
        } else {
          if (word.length > 0) {
            const wordString = word.map(l => l.name).join("");
            if (words.includes(wordString) && !foundWords.includes(wordString)) {
              foundWords.push(wordString);
              setSelectedWords(array => [...array, word]);
            }
            word = [];
          }
        }
      });
      if (word.length > 0) {
        const wordString = word.map(l => l.name).join("");
        if (words.includes(wordString) && !foundWords.includes(wordString)) {
          foundWords.push(wordString);
          setSelectedWords(array => [...array, word]);
        }
      }
    });
  }, [matrix]);


  const handleMouseDown = (rowIndex, colIndex) => {
    setIsSelecting(true);
    const letter = { row: rowIndex, col: colIndex };
    setSelectedLetters([letter]);
  };

  const handleMouseEnter = (rowIndex, colIndex) => {
    if (isSelecting) {
      const letter = { row: rowIndex, col: colIndex };
      setSelectedLetters([...selectedLetters, letter]);
    }
  };

  const handleMouseUp = () => {
    setIsSelecting(false);
    const word = getWordFromSelection(selectedLetters);
    console.log({ word, words });
    if (word && words.map(x => x.name).includes(word.map((letter) => letter.name).join(""))) {
      word.forEach((letter) => {
        letter.selected = true;
        matrix[letter.rowIndex][letter.colIndex] = letter;
      });
      debugger
      setSelectedWords([...selectedWords, word]);
      match(word)
      words.find(w => w.name === word.map(l => l.name).join("")).selected = true;
    }
    setSelectedLetters([]);
  };

  const getWordFromSelection = (selection) => {
    if (selection.length === 0) {
      return null;
    }

    const sortedSelection = selection.sort(
      (a, b) => a.row - b.row || a.col - b.col
    );
    const word = sortedSelection.map(({ row, col }) => matrix[row][col]);

    return word;
  };

  return (
    <div>
      <ul className="list-group list-group-horizontal mb-3 overflow-auto">
        {words.map((word, i) => {
          return (
            <li
              key={i}
              className={clsx('list-group-item', (word.selected && word.found) && 'list-group-item-success', !word.found && 'list-group-item-danger')}
            >
              {word.name}
            </li>
          );
        })}
      </ul>
      {matrix.length > 0 && (
        <table className="table table-bordered">
          <tbody>
            {matrix.map((row, rowIndex) => (
              <tr key={rowIndex}>
                {row.map(({ name, colIndex, selected }) => {
                  const isSelected = selectedLetters.some(
                    ({ row, col }) => row === rowIndex && col === colIndex
                  );
                  return (
                    <td
                      key={colIndex}
                      onMouseDown={() => handleMouseDown(rowIndex, colIndex)}
                      onMouseEnter={() => handleMouseEnter(rowIndex, colIndex)}
                      onMouseUp={handleMouseUp}
                      className={clsx('not-userSelect text-center', selected && 'bg-primary text-light', isSelected && 'bg-warning')}
                    >
                      {name}
                    </td>
                  );
                })}
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default AlphabetSoup;
