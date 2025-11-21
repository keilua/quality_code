import http from 'k6/http';
import { sleep, check } from 'k6';

export const options = {
    stages: [
        { duration: '10s', target: 1000 },
        { duration: '5s', target: 0 },
    ],

    thresholds: {
        'http_req_duration{expected_response:true}': [
            'p(90)<100',    
            'p(95)<200',    
            'p(99)<500',   
        ],

        'http_req_failed': ['rate==0.00'],
    }
};

export default function () {
    const res = http.get('http://localhost:5165/products');

    check(res, {
        'OK response': (r) => r.status === 200,
    });
}
