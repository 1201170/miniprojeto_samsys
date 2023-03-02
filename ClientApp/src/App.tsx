import * as React from 'react';
import { Routes, Route } from 'react-router-dom';
import BookList from './pages/BookList';


export default function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/book-list" element={<BookList />} />
      </Routes>
    </div>
  );
}
