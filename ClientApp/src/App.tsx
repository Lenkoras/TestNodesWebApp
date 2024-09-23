import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Nav from "./components/nav";
import Home from "./components/home";
import NotFound from "./components/notfound";
import './App.scss';

function App()
{
    return (
        <Router>
            <div>
                <Nav />
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="*" element={<NotFound />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;