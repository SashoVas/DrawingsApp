import { TestBed } from '@angular/core/testing';

import { PostFullResolver } from './post-full.resolver';

describe('PostFullResolver', () => {
  let resolver: PostFullResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(PostFullResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
