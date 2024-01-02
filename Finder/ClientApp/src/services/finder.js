const API_URL = "https://localhost:7134/api";

export const generateMatrix = (matrixLength) => {
  return fetch(`${API_URL}/finder/generate/${matrixLength}`)
    .then((response) => response.json())
    .then((data) => data)
    .catch((error) => console.log(error));
};

export const getMatrix = () => {
  return fetch(`${API_URL}/finder`)
    .then((response) => response.json())
    .then((data) => data)
    .catch((error) => console.log(error));
}

export const findAndMatch = (word) => {
  return fetch(`${API_URL}/finder`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(word),
  })
    .then((response) => response.json())
    .then((data) => data)
    .catch((error) => console.log(error));
};

export const findAndSelectWord = (word) => {
  console.log(word);
  return fetch(`${API_URL}/finder/findAndSelectWord`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(word),
  })
    .then((response) => response.json())
    .then((data) => data)
    .catch((error) => console.log(error));
};