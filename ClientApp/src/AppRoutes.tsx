import {BookComponent} from './components/BookComponent';
import BookList from './components/BookList';
import { Counter } from './components/Counter';
import { Home } from './components/Home';


const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/book',
    element: <BookComponent bookIsbn={'111'} bookAuthor={'112'} bookName={'livro1'} bookPrice={29.9} />
  },
  {
    path: '/book-list',
    element: <BookList />
  },
  {
    path: '/counter',
    element: <Counter />
  },
];

export default AppRoutes;