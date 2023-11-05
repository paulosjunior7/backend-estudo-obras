#!/bin/bash

pdm install

eval "$(pdm --pep582)"
