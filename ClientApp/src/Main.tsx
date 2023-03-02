import { Routes, Route } from 'react-router-dom';
import BookList from './pages/BookList';
import BookComponent from './components/BookComponent';

const Main = () => {
    return (         
        <Routes>
        <Route path='/book-list' element={<BookList/>} />
    </Routes>
    );
}
export default Main;