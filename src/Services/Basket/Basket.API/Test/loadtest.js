import http from 'k6/http';

export default function () {
    http.get('http://localhost:5084/api/v1/Basket/mertcan');
}