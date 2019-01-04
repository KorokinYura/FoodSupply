import { AppPage } from './app.po';

describe('FoodSupply App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display application title: FoodSupply', () => {
    page.navigateTo();
    expect(page.getAppTitle()).toEqual('FoodSupply');
  });
});
