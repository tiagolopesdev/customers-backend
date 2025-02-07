import http from 'k6/http';
import { sleep } from 'k6';
import { SharedArray } from 'k6/data';
import execution from 'k6/execution';


const data = new SharedArray("Read Json file", () => {
  return JSON.parse(open('./servos-copy.json'))
})

export const options = {
  vus: 1,
  duration: '20m',
  iterations: data.length
};

export default function () {

  const indexCurrent = execution.scenario.iterationInInstance;

  console.log(`Indice da interação atual: ${indexCurrent} || Cenário: ${execution.scenario.name}`)

  // const url = 'http://localhost:5048/api/Customer/CreateCustomer';
  const url = 'https://minimarket-customer-backend-latest.onrender.com/api/Customer/CreateCustomer';

  const payload = JSON.stringify({
    "name": data[indexCurrent],
    "payments": [],
    "buys": []
  });

  const params = {
    headers: {
      'Content-Type': 'application/json',
      "Accept": "application/json"
    },
  };

  const result = http.post(url, payload, params);

  console.log(`>>> ${indexCurrent} - Result: `, result.status)

  sleep(1);
}
