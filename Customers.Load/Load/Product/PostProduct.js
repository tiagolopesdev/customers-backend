import http from 'k6/http';
import { sleep } from 'k6';
import { SharedArray } from 'k6/data';
import execution from 'k6/execution';


const data = new SharedArray("Read Json file", () => {
  return JSON.parse(open('./data-sp.json'))
  // return JSON.parse(open('./produtos.json'))
  // return JSON.parse(open('./data.json'))
})

export const options = {
  vus: 1,
  duration: '50m',
  iterations: data.length
};

export default function () {

  const indexCurrent = execution.scenario.iterationInInstance;

  console.log('Data ', data.length)
  console.log(`Indice da interação atual: ${indexCurrent} || Cenário: ${execution.scenario.name}`)

  const url = 'https://minimarket-customers-sp-api-prd.onrender.com/api/Products/CreateProduct';
  // const url = 'http://localhost:5048/api/Products/CreateProduct';

  const payload = JSON.stringify(data[indexCurrent]);

  const params = {
    headers: {
      'Content-Type': 'application/json',
      "Accept": "application/json"
    },
  };

  http.post(url, payload, params);
  sleep(1);
}
