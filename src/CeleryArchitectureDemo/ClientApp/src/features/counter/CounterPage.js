import React from "react";
import { Counter } from "./Counter";

const CounterPage = () => (
  <div>
    <h1>Counter</h1>
    <p>This is a simple example of a React component.</p>
    <Counter>
      {({ currentCount: count, increment }) => (
        <>
          <p aria-live="polite">
            Current count: <strong>{count}</strong>
          </p>
          <button className="btn btn-primary" onClick={() => increment()}>
            Increment
          </button>
        </>
      )}
    </Counter>
  </div>
);

export { CounterPage }
