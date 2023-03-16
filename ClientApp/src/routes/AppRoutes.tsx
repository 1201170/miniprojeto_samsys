import {BookComponent} from '../components/OldComponents/BookComponent';
import BookList from '../components/BookTable/BookList';
import { Counter } from '../components/OldComponents/Counter';
import { Home } from '../components/Home/Home';


const AppRoutes = [
  {
    index: true,
    element: <Home />
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