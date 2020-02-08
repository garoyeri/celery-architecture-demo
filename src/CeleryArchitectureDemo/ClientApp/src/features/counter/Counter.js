import { useState } from "react";

function useCounter() {
  const [count, setCount] = useState(0);
  const increment = () => setCount(value => value + 1);
  return { count: count, increment };
}

const Counter = ({ children, ...props }) => children(useCounter(props));

export { Counter, useCounter };
