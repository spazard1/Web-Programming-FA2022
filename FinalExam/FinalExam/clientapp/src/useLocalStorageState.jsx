import { useEffect, useState } from "react";

const useLocalStorageState = (keyName, initialValue) => {
  const [state, setState] = useState(() => {
    if (!localStorage.getItem(keyName)) {
      return initialValue;
    }

    if (initialValue === undefined || typeof initialValue === "number") {
      let localNumber = parseInt(localStorage.getItem(keyName));
      if (!isNaN(localNumber) && isFinite(localStorage.getItem(keyName))) {
        return localNumber;
      }
    }

    if (initialValue === undefined || typeof initialValue === "object") {
      try {
        return JSON.parse(localStorage.getItem(keyName));
      } catch {
        // intentionally empty
      }
    }

    return localStorage.getItem(keyName);
  });

  useEffect(() => {
    if (!keyName) {
      return;
    }

    if (!state) {
      localStorage.removeItem(keyName);
      return;
    }

    let localStorageValue = state;

    if (typeof state === "object" || Array.isArray(state)) {
      localStorageValue = JSON.stringify(state);
    }

    localStorage.setItem(keyName, localStorageValue);
  }, [keyName, state]);

  useEffect(() => {
    if (initialValue && !localStorage.getItem(keyName)) {
      localStorage.setItem(keyName, initialValue);
    }
  }, [keyName, initialValue]);

  return [state, setState];
};

export default useLocalStorageState;
